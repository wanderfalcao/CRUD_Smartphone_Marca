﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_Smartphone_Marca.Model.Exceptions
{
     public class EntityValidationException : Exception
     {
        public string PropertyName { get; }

        public EntityValidationException(string propertyName, string message)
            : base(message)
        {
            PropertyName = propertyName;
        }

        public EntityValidationException(string propertyName, string message, Exception inner)
            : base(message, inner)
        {
            PropertyName = propertyName;
        }
     }
 }

