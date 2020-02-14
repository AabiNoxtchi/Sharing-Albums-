using BlackSound.Entity;
using BlackSound.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackSound.View
{
    public abstract class BaseManagementView<R, E>
        where R : BaseRepository<E>, new()
        where E : BaseEntity, new()
    {
        public void Show()
        {
            while (true)
            {
                string choice = RenderChoice();

                bool cancel = false;
                HandleCustomMenus(choice, ref cancel);
                if (cancel)
                    continue ;

                switch (choice)
                {
                    case "A":
                        {
                            Add();
                            break;
                        }
                    case "V":
                        {
                            View();
                            break;
                        }
                    case "L":
                        {
                            GetAll();
                            break;
                        }

                    case "U":
                        {
                            Update();
                            break;
                        }
                    case "D":
                        {
                            Delete();
                            break;
                        }
                    case "X":
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Choice");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }

        private string RenderChoice()
        {
            Console.Clear();

            BeforeRenderChoice();

            Console.WriteLine(typeof(E).Name.ToString() + " Management");
            Console.WriteLine();
            Console.WriteLine("[A]dd");
            Console.WriteLine("[V]iew");
            Console.WriteLine("[L]ist All");
            Console.WriteLine("[U]pdate");
            Console.WriteLine("[D]elete");
            RenderCustomMenus();
            Console.WriteLine("E[x]it");

            return (Console.ReadLine().ToUpper());
        }

        private void Add()
        {
            Console.Clear();

            Console.WriteLine("Add " + typeof(E).Name);

            E item = new E();
            ReadFromConsole(item);

            R repo = new R();
            repo.Save(item);

            Console.WriteLine("Record saved successfully");
            Console.ReadKey(true);
        }
        private void View()
        {
            Console.Clear();

            E item = GetItem();

            if (item == null)
                return;

            bool cancel = false;
            BeforeView(item, ref cancel);

            if (cancel)
                return;

            WriteToConsole(item);
            Console.ReadKey(true);
        }
        private void GetAll()
        {
            Console.Clear();

            R repo = new R();
            List<E> items = repo.GetAll(GetFilter());

            if (items.Count > 0)
            {
                foreach (E item in items)
                {
                    Console.WriteLine();
                    WriteToConsole(item);
                    Console.WriteLine("################################################");

                   //DisplayGetAllDetails(item);
                }
            }
            else
            {
                Console.WriteLine("No Records Found!");
            }

            Console.ReadKey(true);
        }
        private void Update()
        {
            E item = GetItem();

            if (item == null)
                return;

            Console.WriteLine("Original Record:");
            WriteToConsole(item);
            Console.WriteLine("############################################");

            ReadFromConsole(item);

            R repo = new R();
            repo.Save(item);
            Console.ReadKey(true);
        }
        private void Delete()
        {
            Console.Clear();

            R repo = new R();
            E item = GetItem();

            if (item == null)
                return;
            
            repo.Delete(item);
            Console.WriteLine("Record deleted successfully!");
            Console.ReadKey(true);
        }

        private E GetItem()
        {
            R repo = new R();
            List<E> items = repo.GetAll(GetFilter());
            foreach(E i in items)
            {
                DisplayShort(i);
                Console.Write(" ");
            }
            Console.WriteLine();

            Console.Write(typeof(E).Name + " Id:");
            int id = Convert.ToInt32(Console.ReadLine());

            E item = repo.GetById(id);

            if (item == null)
            {
                Console.WriteLine("Record not found");
                Console.ReadKey(true);
            }

            return item;
        }

        protected virtual void DisplayGetAllDetails(E item) { }
        protected virtual void BeforeView(E item, ref bool cancel) { }
        protected virtual void BeforeRenderChoice() { }
        protected virtual void RenderCustomMenus() { }
        protected virtual void HandleCustomMenus(string choice, ref bool cancel) { }
		


		protected virtual Func<E, bool> GetFilter() { return null; }
        protected abstract void ReadFromConsole(E item);
        protected abstract void WriteToConsole(E item);
        protected abstract void DisplayShort(E item);
    }
}
