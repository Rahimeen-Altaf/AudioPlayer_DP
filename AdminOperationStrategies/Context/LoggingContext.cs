using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.AdminOperationStrategies.Context
{
    internal class LoggingContext : AdminOperationContext
    {
        public override void ExecuteStrategy(string username)
        {
            Console.WriteLine($"[LOG] Executing {_strategy?.GetType().Name} for {username} at {DateTime.Now}");
            base.ExecuteStrategy(username);
        }
    }
}