﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkingAuxiliaryLibrary.Objects.Common
{
    public class MessageDTO
    {
        public string Sender { get; set; }

        public string Contents { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MessageDTO()
        {

        }

    }
}