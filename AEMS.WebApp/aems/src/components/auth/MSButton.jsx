import React from "react";
import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../../services/authConfig";
import Button from "react-bootstrap/Button";


/**
 * Renders a button which, when selected, will redirect the page to the login prompt
 */
export const MSSignInButton = () => {
    const { instance } = useMsal();
    function handleLogin() {
        instance.loginRedirect(loginRequest).catch(e => {
            console.error(e);
        });
    }
    return (
        <Button variant="secondary" className="ml-auto" onClick={() => handleLogin()}>Sign In </Button>
    );
}

export const MSSignOutButton = () => {
    const { instance } = useMsal();
    const handleLogout = () => {
        instance.logoutRedirect({
            postLogoutRedirectUri: "/",
        });
    }
    return (
        <Button variant="secondary" className="ml-auto" onClick={() => handleLogout(instance)}>Sign Out</Button>
    )
}