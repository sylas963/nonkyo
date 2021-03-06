﻿#region License
/*Copyright (c) 2013, Karl Sparwald
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that 
the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following 
disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the 
following disclaimer in the documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS
OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE 
COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER 
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED 
OF THE POSSIBILITY OF SUCH DAMAGE.*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NOnkyo.ISCP.Command
{
    public class SubwooferLevel : CommandBase
    {
        public static readonly SubwooferLevel State = new SubwooferLevel()
        {
            CommandMessage = "SWLQSTN"
        };

        public static readonly SubwooferLevel Up = new SubwooferLevel()
        {
            CommandMessage = "SWLUP"
        };

        public static readonly SubwooferLevel Down = new SubwooferLevel()
        {
            CommandMessage = "SWLDOWN"
        };

        public static SubwooferLevel SetLevel(int pnLevel)
        {
            string lsCommandMessage = "SWL{0}";
            return new SubwooferLevel()
            {
                CommandMessage = lsCommandMessage.FormatWith(pnLevel.ConvertIntToDbValue())
            };
        }

        #region Constructor / Destructor

        internal SubwooferLevel()
        { }

        #endregion

        public int? Level { get; private set; }

        public string Display { get; private set; }

        public bool CanLevelDown()
        {
            return this.Level.GetValueOrDefault() > -15;
        }

        public bool CanLevelUp()
        {
            return this.Level.GetValueOrDefault() < 12;
        }

        public override bool Match(string psStatusMessage)
        {
            var loMatch = Regex.Match(psStatusMessage, @"!1SWL(.\w)");
            if (loMatch.Success)
            {
                this.Level = loMatch.Groups[1].Value.ConvertDbValueToInt();
                this.Display = this.Level.ConvertDbIntValueToDisplay();
                return true;
            }
            return false;
        }

    }
}
