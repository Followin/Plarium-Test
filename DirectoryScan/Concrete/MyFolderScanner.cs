using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using DirectoryScan.Abstract;
using DirectoryScan.Models;

namespace DirectoryScan.Concrete
{
    public class MyFolderScanner
    {
        /// <summary>
        /// Добавляет информацию о директории в очередь
        /// </summary>
        /// <param name="path">Путь к директории</param>
        /// <param name="queue">Ссылка на очередь</param>
        /// <param name="recLevel">Уровень рекурсии(не менять)</param>
        public static void GetDirectories(string path, IDirectoryBlockingQueue queue, int recLevel = 0)
        {
            
                var directory = new DirectoryInfo(path);
            try
            {
                //Основная информация о папке
                var directoryModel = new DirectoryViewModel();
                directoryModel.Path = path;
                directoryModel.Name = directory.Name;
                directoryModel.LastAccessTime = directory.LastAccessTime;
                directoryModel.CreationTime = directory.CreationTime;
                directoryModel.LastWriteTime = directory.LastWriteTime;

                directoryModel.AttributesInfo.Archive = directory.Attributes.HasFlag(FileAttributes.Archive);
                directoryModel.AttributesInfo.Compresed = directory.Attributes.HasFlag(FileAttributes.Compressed);
                directoryModel.AttributesInfo.Encrypted = directory.Attributes.HasFlag(FileAttributes.Encrypted);
                directoryModel.AttributesInfo.Hidden = directory.Attributes.HasFlag(FileAttributes.Hidden);
                directoryModel.AttributesInfo.ReadOnly = directory.Attributes.HasFlag(FileAttributes.ReadOnly);
                directoryModel.AttributesInfo.System = directory.Attributes.HasFlag(FileAttributes.System);

                //Информация о правах
                var directoryAccessControl = directory.GetAccessControl();
                try
                {
                    directoryModel.AccessInfo.Owner = directoryAccessControl.GetOwner(typeof (NTAccount)).Value;
                }
                catch
                {
                    directoryModel.AccessInfo.Owner = "No Info";
                }

                //Получаем права на папку текущего пользователя или его группы
                var directoryRulesCollection = directoryAccessControl.GetAccessRules(true, true, typeof (NTAccount));
                var groupsCollection =
                    WindowsIdentity.GetCurrent().Groups.Select(_ => _.Translate(typeof (NTAccount)).Value).ToList();
                var currentUserDirectoryRulesCollection = directoryRulesCollection.Cast<FileSystemAccessRule>()
                    .Where(
                        singleRule =>
                            string.Equals(singleRule.IdentityReference.Value, WindowsIdentity.GetCurrent().Name,
                                StringComparison.InvariantCultureIgnoreCase)
                            || groupsCollection.Contains(singleRule.IdentityReference.Value)).ToList();

                //Записываем права в модель
                directoryModel.AccessInfo.Modify =
                    currentUserDirectoryRulesCollection.Exists(_ => _.FileSystemRights.HasFlag(FileSystemRights.Modify));
                directoryModel.AccessInfo.Read =
                    currentUserDirectoryRulesCollection.Exists(_ => _.FileSystemRights.HasFlag(FileSystemRights.Read));
                directoryModel.AccessInfo.ReadExecute =
                    currentUserDirectoryRulesCollection.Exists(
                        _ => _.FileSystemRights.HasFlag(FileSystemRights.ReadAndExecute));
                directoryModel.AccessInfo.Write =
                    currentUserDirectoryRulesCollection.Exists(_ => _.FileSystemRights.HasFlag(FileSystemRights.Write));


                //Проворачиваем тоже самое для файлов
                foreach (var fileInfo in directory.GetFiles())
                {
                    var file = new FileViewModel();
                    file.Name = fileInfo.Name;
                    file.Size = fileInfo.Length/1024;
                    file.CreationTime = fileInfo.CreationTime;
                    file.LastAccessTime = fileInfo.LastAccessTime;
                    file.LastWriteTime = fileInfo.LastWriteTime;

                    file.AttributesInfo.Archive = fileInfo.Attributes.HasFlag(FileAttributes.Archive);
                    file.AttributesInfo.Compresed = fileInfo.Attributes.HasFlag(FileAttributes.Compressed);
                    file.AttributesInfo.Encrypted = fileInfo.Attributes.HasFlag(FileAttributes.Encrypted);
                    file.AttributesInfo.Hidden = fileInfo.Attributes.HasFlag(FileAttributes.Hidden);
                    file.AttributesInfo.ReadOnly = fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly);
                    file.AttributesInfo.System = fileInfo.Attributes.HasFlag(FileAttributes.System);


                    var fileAccessControl = fileInfo.GetAccessControl();

                    try
                    {
                        file.AccessInfo.Owner = fileAccessControl.GetOwner(typeof (NTAccount)).Value;
                    }
                    catch
                    {
                        file.AccessInfo.Owner = "No Info";
                    }

                    var fileRulesCollection = fileAccessControl.GetAccessRules(true, true, typeof (NTAccount));
                    var currentUserFileRulesCollection = fileRulesCollection.Cast<FileSystemAccessRule>()
                        .Where(
                            singleRule =>
                                string.Equals(singleRule.IdentityReference.Value, WindowsIdentity.GetCurrent().Name,
                                    StringComparison.InvariantCultureIgnoreCase)
                                || groupsCollection.Contains(singleRule.IdentityReference.Value)).ToList();

                    //Записываем права в модель
                    file.AccessInfo.Modify =
                        currentUserFileRulesCollection.Exists(_ => _.FileSystemRights.HasFlag(FileSystemRights.Modify));
                    file.AccessInfo.Read =
                        currentUserFileRulesCollection.Exists(_ => _.FileSystemRights.HasFlag(FileSystemRights.Read));
                    file.AccessInfo.ReadExecute =
                        currentUserFileRulesCollection.Exists(
                            _ => _.FileSystemRights.HasFlag(FileSystemRights.ReadAndExecute));
                    file.AccessInfo.Write =
                        currentUserFileRulesCollection.Exists(_ => _.FileSystemRights.HasFlag(FileSystemRights.Write));

                    directoryModel.Files.Add(file);

                }


                queue.Enqueue(directoryModel);


                foreach (var dir in directory.GetDirectories())
                {
                    GetDirectories(dir.FullName, queue, recLevel + 1);
                }
            }
            catch
            { }


            if (recLevel == 0)
                queue.End();

        }

    }
}
