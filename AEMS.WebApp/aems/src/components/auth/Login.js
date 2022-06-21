import * as React from 'react';
import { useState, useEffect } from "react"
import { useDispatch, useSelector } from "react-redux";
import { loginUser, loginAzure } from "../../slices/authSlice";
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
import PixIcon from '@mui/icons-material/Pix';
import { useMsal } from "@azure/msal-react";
import { loginRequest } from "../../slices/authSlice";
import { useIsAuthenticated } from "@azure/msal-react";
import Home from '../../components/Home'

const theme = createTheme();

const Login = () => {
  const navigate = useNavigate()
  const dispatch = useDispatch()
  const auth = useSelector((state) => state.auth)
  const { instance } = useMsal();
  const [user, setUser] = useState({
    email: "",
    password: "",
  })
  const isAuthenticated = useIsAuthenticated();

  useEffect(() => {
    if (auth._id) {
      navigate("/home")
    } else {
      navigate("/login")
    }
  }, [auth?._id, navigate]);

  const handleSubmit = (event) => {
    event.preventDefault();
    dispatch(loginUser(user))
    const data = new FormData(event.currentTarget);
  };

  const handleLogin = (instance) => {
    instance.loginPopup(loginRequest)
    .then(res => {
      console.log(res);
      var responseModel = {
        accessToken: res.accessToken,
        name: res.account.name,
        email : res.account.username,
        _id : res.account.idTokenClaims.oid
      };
      dispatch(loginAzure(responseModel))
    })
    .catch(e => {
        console.error(e);
<<<<<<< HEAD
      })
=======
    })
>>>>>>> origin/develop
  }

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
              onChange={(e) => setUser({ ...user, email: e.target.value })}
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
              onChange={(e) => setUser({ ...user, password: e.target.value })}
              autoComplete="current-password"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              {auth.loginStatus === "pending" ? "Submitting ..." : "Login"}
            </Button>
            <Button variant="contained" fullWidth color="warning" startIcon={<PixIcon />} onClick={() => handleLogin(instance)}>
              Login Azure
            </Button>
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