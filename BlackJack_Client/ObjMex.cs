﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack_Client
{
    /// <summary>
    /// Messaggio standard
    /// </summary>
    internal class ObjMex
    {
        private string _action;
        private List<dynamic> _multipleData;
        private dynamic _singleData;

        public string Action { get => _action; set => _action = value; }
        public List<dynamic> MultipleData { get => _multipleData; set => _multipleData = value; }
        public dynamic SingleData { get => _singleData; set => _singleData = value; }

        //Quando si passano più valori
        public ObjMex(string action, List<dynamic> data)
        {
            this._action = action;
            this._multipleData = data;
        }
        //Quando si passa un singolo valore
        public ObjMex(string action, dynamic data)
        {
            this._action = action;
            this._singleData = data;
        }
    }
}
