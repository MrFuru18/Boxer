using ApiLibrary.Model;
using ApiLibrary.Model.ToCreate;
using ApiLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Boxer.Commands
{
    public class EditOrderItemCommand : CommandBase
    {
        private OrderItem _orderItem;

        public EditOrderItemCommand(OrderItem orderItem)
        {
            _orderItem = orderItem;
        }

        public override void Execute(object p)
        {
            string result = OrderProcessor.editOrderItem(_orderItem).Result;
            if (result == "OK")
            {
                bool taskExist = false;
                ToCreateTaskState ts = new ToCreateTaskState();
                List<Tasks> tasksAll = new List<Tasks>(TaskProcessor.getAllTasks(new Tasks()).Result);
                if (tasksAll.Count > 0)
                {
                    for (int i = tasksAll.Count - 1; i >= 0; i--)
                    {
                        if (tasksAll[i].order_id == _orderItem.order_id)
                        {
                            ts.task_id = tasksAll[i].id;
                            taskExist = true;
                            break;

                        }
                    }
                }

                if (taskExist)
                {
                    ts.state = "modified";
                    string r = TaskProcessor.addTaskState(ts).Result;
                    if (r != "Created")
                        MessageBox.Show(r);
                }
            }
            else
                MessageBox.Show(result);
        }
    }
}
