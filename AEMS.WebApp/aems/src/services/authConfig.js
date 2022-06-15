import { LogLevel } from "@azure/msal-browser";

export const msalConfig = {
    auth: {
      clientId: "00a21f64-cd60-4444-8e0f-03c9f4d36762",
      authority: "https://login.microsoftonline.com/0571822d-33dc-455e-a07f-1b5575297583", 
      redirectUri: "http://localhost:3000"
    },
    cache: {
      cacheLocation: "sessionStorage", // This configures where your cache will be stored
      storeAuthStateInCookie: true, // Set this to "true" if you are having issues on IE11 or Edge
    },
    system: {
        loggerCallback: (level, message, containsPii) => {	
            if (containsPii) {		
                return;		
            }		
            switch (level) {		
                case LogLevel.Error:		
                    console.error(message);		
                    return;		
                case LogLevel.Info:		
                    console.info(message);		
                    return;		
                case LogLevel.Verbose:		
                    console.debug(message);		
                    return;		
                case LogLevel.Warning:		
                    console.warn(message);		
                    return;		
            }	
        }
    }
  };


export const loginRequest = {
    scopes: ["User.Read"]
};

export const graphConfig = {
    graphMeEndpoint: "https://graph.microsoft.com/v1.0/me"
};