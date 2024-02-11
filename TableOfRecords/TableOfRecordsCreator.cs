using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TableOfRecords
{
    /// <summary>
    /// Presents method that write in table form to the text stream a set of elements of type T.
    /// </summary>
    public static class TableOfRecordsCreator
    {
        /// <summary>
        /// Write in table form to the text stream a set of elements of type T (<see cref="ICollection{T}"/>),
        /// where the state of each object of type T is described by public properties that have only build-in
        /// type (int, char, string etc.)
        /// </summary>
        /// <typeparam name="T">Type selector.</typeparam>
        /// <param name="collection">Collection of elements of type T.</param>
        /// <param name="writer">Text stream.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="collection"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="writer"/> is null.</exception>
        /// <exception cref="ArgumentException">Throw if <paramref name="collection"/> is empty.</exception>
        public static void WriteTable<T>(ICollection<T>? collection, TextWriter? writer)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            if (collection.Count == 0)
            {
                throw new ArgumentException("Collection cannot be empty.", nameof(collection));
            }

            var properties = typeof(T).GetProperties();

            var columnWidths = new Dictionary<string, int>();
            foreach (var property in properties)
            {
                var maxWidth = Math.Max(property.Name.Length, collection.Max(item =>
                {
                    var value = property.GetValue(item)?.ToString() ?? string.Empty;
                    return value.Length;
                }));
                columnWidths[property.Name] = maxWidth;
            }

            WriteRow(properties.Select(p => p.Name).ToArray(), columnWidths, writer);

            WriteSeparator(columnWidths, writer);

            foreach (var item in collection)
            {
                var rowValues = properties.Select(p => p.GetValue(item)?.ToString() ?? string.Empty).ToArray();
                WriteRow(rowValues, columnWidths, writer);
            }
        }

        private static void WriteRow(string[] values, Dictionary<string, int> columnWidths, TextWriter writer)
        {
            for (int i = 0; i < values.Length; i++)
            {
                writer.Write("| ");
                writer.Write(values[i].PadRight(columnWidths.Values.ElementAt(i)));
                writer.Write(" ");
            }

            writer.WriteLine("|");
        }

        private static void WriteSeparator(Dictionary<string, int> columnWidths, TextWriter writer)
        {
            foreach (var width in columnWidths.Values)
            {
                writer.Write("+" + new string('-', width + 2));
            }

            writer.WriteLine("+");
        }
    }
}
