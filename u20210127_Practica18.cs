using System;
using System.IO;

namespace MyApp// Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "datos.bin";

            // Escribir datos aleatorios en el archivo
            EscribirDatosAleatorios(filePath);

            // Leer datos aleatorios del archivo
            LeerDatos(filePath);

            Console.ReadLine();
        }
        static void EscribirDatosAleatorios(string filePath)
        {
            Random random = new Random();

            // Abrir el archivo en modo de escritura
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                // Escribir datos aleatorios en ubicaciones específicas
                EscribeDatoEnPosicion(writer, 0, GenerarNumeroAleatorio(random));
                EscribeDatoEnPosicion(writer, 1, GenerarNumeroAleatorio(random));
                EscribeDatoEnPosicion(writer, 2, GenerarNumeroAleatorio(random));

                Console.WriteLine("\nDatos escritos en el archivo.");
            }
        }

        static int GenerarNumeroAleatorio(Random random)
        {
            // Generar un número aleatorio entre 0 y 255 (un byte)
            return random.Next(256);
        }

        static void EscribeDatoEnPosicion(BinaryWriter writer, int posicion, int dato)
        {
            // Calcular la posición en bytes
            long bytePosicion = posicion * sizeof(int);

            // Mover el puntero de escritura a la posición deseada
            writer.Seek((int)bytePosicion, SeekOrigin.Begin);

            // Escribir el dato en la posición especificada
            writer.Write(dato);
        }

        static void LeerDatos(string filePath)
        {
            // Abrir el archivo en modo de lectura
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                // Leer datos de ubicaciones específicas
                Console.WriteLine("Dato en posición 0: " + LeeDatoEnPosicion(reader, 0));
                Console.WriteLine("Dato en posición 1: " + LeeDatoEnPosicion(reader, 1));
                Console.WriteLine("Dato en posición 2: " + LeeDatoEnPosicion(reader, 2));
            }
        }

        static int LeeDatoEnPosicion(BinaryReader reader, int posicion)
        {
            // Calcular la posición en bytes
            long bytePosicion = posicion * sizeof(int);

            // Mover el puntero de lectura a la posición deseada
            reader.BaseStream.Seek(bytePosicion, SeekOrigin.Begin);

            // Leer el dato en la posición especificada
            return reader.ReadInt32();
        }
    }
}