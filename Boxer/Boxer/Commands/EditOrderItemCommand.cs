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
            if (checkIfCorrect())
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
                            MessageBox.Show("Nie udało się uaktualnić statusu zadania");
                    }
                }
                else
                    MessageBox.Show("Nie udało się edytować artykułu w zamówieniu");
            }
        }

        private bool checkIfCorrect()
        {
            if (_orderItem.quantity == null)
            {
                MessageBox.Show("Ilość produktów nie może być pusta");
                return false;
            }

            if (_orderItem.product_id == null)
            {
                MessageBox.Show("Id produktu nie może być puste");
                return false;
            }

            List<Product> prodList = ProductProcessor.getAllProducts(new Product()).Result;
            bool prodExist = false;
            foreach (var prod in prodList)
                if (prod.id == _orderItem.product_id)
                    prodExist = true;

            if (!prodExist)
            {
                MessageBox.Show("Id produktu nie istnieje");
                return false;
            }

            return true;
        }

    }
}
