/*
 * Copyright (c) Contributors, http://aurora-sim.org/, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the Aurora-Sim Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using Aurora.Framework;
using OpenSim.Region.CoreModules.Framework.Monitoring.Monitors;

namespace OpenSim.Region.CoreModules.Framework.Monitoring.Alerts
{
    internal class DeadlockAlert : IAlert
    {
        private readonly LastFrameTimeMonitor m_monitor;

        public DeadlockAlert(LastFrameTimeMonitor m_monitor)
        {
            this.m_monitor = m_monitor;
        }

        #region Implementation of IAlert

        public string GetName()
        {
            return "Potential Deadlock Alert";
        }

        public void Test()
        {
            if (m_monitor.GetValue() > 60*1000)
            {
                if (OnTriggerAlert != null)
                {
                    OnTriggerAlert(typeof (DeadlockAlert),
                                   (int) (m_monitor.GetValue()/1000) + " second(s) since last frame processed.", true);
                }
            }
        }

        public event Alert OnTriggerAlert;

        #endregion
    }
}