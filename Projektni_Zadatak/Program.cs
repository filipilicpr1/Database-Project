using Projektni_Zadatak.DAO;
using Projektni_Zadatak.DAO.Impl;
using Projektni_Zadatak.Model;
using Projektni_Zadatak.UIHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektni_Zadatak
{
    class Program
    {
        private static readonly QueryUIHandler queryUIHandler = new QueryUIHandler();
        static void Main(string[] args)
        {
            queryUIHandler.HandleQueryMenu();
        }
    }
}
