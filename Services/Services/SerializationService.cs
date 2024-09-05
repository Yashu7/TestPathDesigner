using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Windows.Shapes;

namespace Services.Services
{
    public class SerializationService<T>
    {
        public void SerializeObject(T obj)
        {
            var serializedObject = JsonSerializer.Serialize(obj);
            Console.WriteLine(serializedObject);
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.SaveFileDialog();

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string folderName = dialog.SafeFileName;
                using (var stream = new FileStream(dialog.FileName,FileMode.Create))
                {
                    stream.Write(Encoding.Default.GetBytes(serializedObject), 0, serializedObject.Length);
                }
            }
        }
        public T DeserializeObject()
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                string readText = File.ReadAllText(dialog.FileName);
                return JsonSerializer.Deserialize<T>(readText);
            }
            return default;
        }
    }
}
