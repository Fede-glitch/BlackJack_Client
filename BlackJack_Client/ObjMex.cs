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
        private dynamic _data;

        public string Action { get => _action; set => _action = value; }
        public dynamic Data { get => _data; set => _data = value; }

        public ObjMex(string action, dynamic data)
        {
            this._action = action;
            this._data = data;
        }
    }
}
