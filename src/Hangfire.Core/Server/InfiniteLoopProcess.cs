﻿// This file is part of Hangfire.
// Copyright © 2015 Sergey Odinokov.
// 
// Hangfire is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as 
// published by the Free Software Foundation, either version 3 
// of the License, or any later version.
// 
// Hangfire is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public 
// License along with Hangfire. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Threading;
using Hangfire.Annotations;

namespace Hangfire.Server
{
    internal class InfiniteLoopProcess : IBackgroundProcess
    {
        public InfiniteLoopProcess([NotNull] IServerProcess innerProcess)
        {
            if (innerProcess == null) throw new ArgumentNullException("innerProcess");
            InnerProcess = innerProcess;
        }

        public IServerProcess InnerProcess { get; private set; }

        public void Execute(BackgroundProcessContext context)
        {
            while (true)
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                InnerProcess.Execute(context);
            }
        }

        public override string ToString()
        {
            return InnerProcess.ToString();
        }
    }
}