using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonAlgorithms.ArraySegments
{
   
    public class AsyncFileReader
    {       
        public async Task ProcessReadAsync(string filePath)
        {
            try
            {
               
                if (File.Exists(filePath) != false)
                {                    
                    List<Task> fileReadTasks = (List<Task>)await BuildFileReadTasks(filePath);

                    await Task.WhenAll(fileReadTasks.ToArray());
                }
                else
                {
                    Console.WriteLine($"file not found: {filePath}");
                }
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Errors occurred when working with the array:");
                foreach (var inner in e.InnerExceptions)
                    Console.WriteLine("{0}: {1}", inner.GetType().Name, inner.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private static async Task<IList<Task>> BuildFileReadTasks(string filePath)
        {
            IList<Task> fileReadTasks = new List<Task>();

            using var sourceStream = 
              new FileStream(
                  filePath,
                  FileMode.Open, FileAccess.Read, FileShare.Read,
                  bufferSize: 4096, useAsync: true);

            IArraySegmentBuilder<byte> fileArraySegmentBuilder = new FileReadArraySegmentBuilder(sourceStream);

            IList<ArraySegment<byte>> fileReadArraySegments = (IList<ArraySegment<byte>>)await fileArraySegmentBuilder.Build(4096);

            fileReadArraySegments.ToList().ForEach(item =>
            {
                fileReadTasks.Add(Task.Run(() =>
                {
                   Console.WriteLine(Encoding.ASCII.GetString(item));
                }));
            });

            return fileReadTasks;            
        }
    }
}
