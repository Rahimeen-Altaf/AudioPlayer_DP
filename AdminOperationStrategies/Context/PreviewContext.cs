using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer.AdminOperationStrategies.Context
{
    internal class PreviewContext : AdminOperationContext
    {
        public override void ExecuteStrategy(string username)
        {
            Console.WriteLine($"[PREVIEW] This would run: {_strategy?.GetType().Name} for user '{username}'");
        }
    }
}