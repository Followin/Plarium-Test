
using DirectoryScan.Models;

namespace DirectoryScan.Abstract
{
    public interface IDirectoryBlockingQueue
    {
        /// <summary>
        /// Добавляет информацию о директории в очередь
        /// </summary>
        /// <param name="model">Информация о файле</param>
        void Enqueue(DirectoryViewModel model);

        /// <summary>
        /// Берет информацию о директории из очереди
        /// </summary>
        /// <param name="model">Информация о директории</param>
        /// <returns>Удалось ли</returns>
        bool TryDequeue(out DirectoryViewModel model);

        /// <summary>
        /// Завершает добавление в очередь
        /// </summary>
        void End();

        /// <summary>
        /// Завершена ли очередь
        /// </summary>
        bool Ended { get; }

        /// <summary>
        /// Кол-во элементов в ней
        /// </summary>
        int Count { get; }
    }
}
