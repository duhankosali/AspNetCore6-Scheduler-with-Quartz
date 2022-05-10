namespace AspNetCore_QuartzExample.Models
{
    public class MyJob
    {
        public MyJob(Type type, string expression) // constructor
        {
            Common.Logs($"MyJob at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"), "MyJob" + DateTime.Now.ToString("hhmmss"));

            Type = type;
            Expression = expression;
        }

        // properties
        public Type Type { get; set; }
        public string Expression { get; set; }  
    }
}
