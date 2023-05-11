using System;
using System.Collections.Generic;

/*
    NOMBRE: Zambrano Anchundia Julio César
    C.I: 131520217-4
*/
/*
                DESARROLAR UN PROGRAMA QUE SIMULE UN CAJERO AUTOMATICO
    Se tiene la siguiente información:
    1. Los clientes llegan al cajero cada 20 - 30 min.
    2. Cada cliente tarda entre 3 - 5 min en ser atendido.

    OBTENER LA SIGUIENTE INFORMACIÓN
    PUNTO 1: Cantidad de clientes que se atienden en 12 horas.
    PUNTO 2: Cantidad de cientes que hay en cola despues de 12 horas.
    PUNTO 3: Hora de llegada del primer cliente que no es atendido luego de las 12 horas. (es decir, la persona que 
    está en la cola cuando se cumplen las 12 horas.)

    RESPUESTA:
    PUNTO 1:Cantidad de clientes atendidos cada 12 horas: 30
    PUNTO 2:Cantidad de clientes en la cola después de 12 horas: 0
    PUNTO 3:Hora de llegada del primer cliente no atendido después de 12 horas: 362
*/

namespace CajeroAutomatico
{
    class Program
    {
        static void Main(string[] args)
        {
            
            const int simulationDuration = 12 * 60; // Duración de la simulación en minutos (12 horas)
            const int minTiempo = 20; // Tiempo mínimo de llegada de clientes en minutos
            const int maxTiempo = 30; // Tiempo máximo de llegada de clientes en minutos
            const int minServicio = 3; // Tiempo mínimo de atención de clientes en minutos
            const int maxServicio = 5; // Tiempo máximo de atención de clientes en minutos

            // se crean las variables indicadas como números y nombres aleatorios
            var random = new Random();
            var clienteServido = 0;
            var clienteInQueue = 0;
            var queue = new Queue<int>();
            var nextClienteLLegada = 0;
            var primerUsuario = -1;

            for (int time = 0; time < simulationDuration; time++)
            {
                // Ingresa al ciclo para verificar si llegan clientes
                if (time == nextClienteLLegada)
                {
                    var serviceTime = random.Next(minServicio, maxServicio + 1);
                    if (clienteInQueue == 0)
                    {
                        clienteServido++;
                    }
                    else
                    {
                        queue.Enqueue(serviceTime);
                        clienteInQueue++;
                    }

                    nextClienteLLegada = time + random.Next(minTiempo, maxTiempo + 1);
                }

                // Se atiende al cliente 
                if (clienteInQueue > 0)
                {
                    var remainingServiceTime = queue.Peek() - 1;
                    if (remainingServiceTime == 0)
                    {
                        queue.Dequeue();
                        clienteInQueue--;
                        clienteServido++;
                    }
                    else
                    {
                        queue.Dequeue();
                        queue.Enqueue(remainingServiceTime);
                    }
                }

                // Verificar si ha pasado 12 horas
                if (time == simulationDuration / 2 && primerUsuario == -1)
                {
                    primerUsuario = nextClienteLLegada;
                }
            }

            Console.WriteLine("PUNTO 1:"+"Cantidad de clientes atendidos cada 12 horas: " + clienteServido);
            Console.WriteLine("PUNTO 2:"+"Cantidad de clientes en la cola después de 12 horas: " + clienteInQueue);
            Console.WriteLine("PUNTO 3:"+"Hora de llegada del primer cliente no atendido después de 12 horas: " + primerUsuario);

            Console.ReadLine();
        }
    }
}