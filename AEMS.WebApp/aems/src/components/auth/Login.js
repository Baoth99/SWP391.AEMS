import * as React from 'react';
import {useState, useEffect} from "react"
import { useDispatch, useSelector } from "react-redux";
import { loginUser } from "../../slices/authSlice";
import { useNavigate } from "react-router-dom";
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { MSSignInButton } from "./MSButton";

import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../../services/authConfig";

const theme = createTheme();

const Login = () => {
  const navigate = useNavigate()
  const dispatch = useDispatch()
  const auth = useSelector((state) => state.auth)

  const { instance, accounts } = useMsal(); //MS

  function GetUserInfo() {
    // Silently acquires an access token which is then attached to a request for MS Graph data
    instance.acquireTokenSilent({
        ...loginRequest,
        account: accounts[0] 
    }).then((response) => {
        var name = response.account.name;
        var username = response.account.username
        var oid = response.account.idTokenClaims.oid;
        var accessToken = response.accessToken

        // TODO:
        console.log(name);
        console.log(username);
        console.log(oid);
        console.log(accessToken);
    });
}


  const [user, setUser] = useState({
    email: "",
    password: "",
  })

  useEffect(() => {
    if(auth._id) {
      navigate("/home")
    } else {
      navigate("/login")
    }
  }, [auth._id, navigate])

  const handleSubmit = (event) => {
    event.preventDefault();
    console.log(user.name);
    dispatch(loginUser(user))
    const data = new FormData(event.currentTarget);
    console.log({
      email: data.get('email'),
      password: data.get('password'),
    });
  };

  return (
    <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
            <TextField
              margin="normal"
              required
              fullWidth
              id="email"
              label="Email Address"
              name="email"
              type="email"
              onChange={(e) => setUser({...user, email: e.target.value})}
              autoComplete="email"
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              id="password"
              onChange={(e) => setUser({...user, password: e.target.value})}
              autoComplete="current-password"
            />
            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Remember me"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              {auth.loginStatus === "pending" ? "Submitting ..." : "Login"}
            </Button>
            <MSSignInButton/>
            {auth.loginStatus === "rejected" ? <p>{auth.loginError}</p> : null}
            <Grid container>
              <Grid item xs>
                <Link href="#" variant="body2">
                  Forgot password?
                </Link>
              </Grid>
              <Grid item>
                <Link href="#" variant="body2">
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
}

export default Login;