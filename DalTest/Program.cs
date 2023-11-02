using Dal;
using DalApi;


namespace DalTest
{
    internal class Program
    {
        private static IEngineer? e_dalIEngineer = new EngineerImplementation(); //Stage 1
        private static ITask? t_dalITask = new TaskImplementation(); //Stage 1
        private static IDependence? d_dalIDependence = new DependenceImplementation(); //Stage 1

       static void Main(string[] args)
       {
            try
            {
                Initialization.Do(t_dalITask,e_dalIEngineer,d_dalIDependence);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
            }
       } 
        
    }
}