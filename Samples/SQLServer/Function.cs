using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace Samples.SQLServer
{
    public class Function
    {
        private readonly ILogger _logger;

        public Function(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function>();
        }

        [Function("SqlServerChange")]
        public void Run(
            [SqlTrigger("[dbo].[ToDo]", "SqlConnectionString")] IReadOnlyList<SqlChange<ToDoItem>> changes)
        {
            _logger.LogInformation($"SQL Changes: {changes.Count}");
            foreach (var item in changes)
            {
                _logger.LogInformation($"Operation: {item.Operation} - [{item.Item}]");
            }
        }
    }

    public class ToDoItem
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public string ToDo { get; set; }
        public string Details { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, DueDate: {DueDate}, ToDo: {ToDo}";
        }


    }
}
