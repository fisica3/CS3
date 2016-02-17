using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

using PlainConcepts.LinqToTfs;

namespace Ejemplo11_05
{
    class Program
    {
        static void Main(string[] args)
        {
            // *** VIA ESTANDAR

            // Nos conectamos al TFS
            TeamFoundationServer server = TeamFoundationServerFactory.GetServer("http://tfs.plainconcepts.com:4040/");
            // Obtenemos una referencia a su WorkItemStore
            WorkItemStore workItemStore = (WorkItemStore)server.GetService(typeof(WorkItemStore));
//            // La consulta
//            string query =
//                @"SELECT Id, Title, [Created By], [Created Date] FROM WorkItems WHERE 
//                  [System.TeamProject] = 'Lebab' AND [Created By] = 'Marco Amoedo Martínez'
//                  ORDER BY [Created Date]";
//            // Ejecutamos la consulta
//            WorkItemCollection collection = workItemStore.Query(query);
//            // Recuperamos los resultados
//            Console.WriteLine("* TFS API *");
//            foreach (WorkItem i in collection)
//                Console.WriteLine(i.Id + " | " + i.Title + " | " + i.CreatedDate + " | " + i.CreatedBy);

            // *** LINQ WAY

            var q = from i in workItemStore.ToQueryable()
                    where i.Project.Name == "Lebab"
                    orderby i.CreatedDate
                    select i;
            Console.WriteLine("* LINQ TO TFS *");
            foreach (var i in q)
                Console.WriteLine(i.Id + " | " + i.Title + " | " + i.CreatedDate + " | " + i.CreatedBy);
                   
            Console.ReadLine();
        }
    }
}
