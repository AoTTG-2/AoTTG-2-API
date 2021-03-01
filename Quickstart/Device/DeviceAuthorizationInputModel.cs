// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using AoTTG2.IDS.Quickstart.Consent;

namespace AoTTG2.IDS.Quickstart.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}