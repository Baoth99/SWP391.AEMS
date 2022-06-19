import './App.css';
import { BrowserRouter, Routes, Route, Navigate, useLocation, Outlet } from "react-router-dom";
import Login from './components/auth/Login';
import Register from './components/auth/Register';
import Home from './components/Home';
import "react-toastify/dist/ReactToastify.css";
import { loadUser } from "./slices/authSlice";
import { useEffect } from "react";
import { ToastContainer } from "react-toastify";
import { useDispatch, useSelector } from "react-redux";
import NotFound from './components/error/NotFound';
import { useIsAuthenticated } from "@azure/msal-react";
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";

function RequireAuth() {
  const auth = useSelector((state) => state.auth)
  let location = useLocation();

  if (!auth._id) {
    // Redirect them to the /login page, but save the current location they were
    // trying to go to when they were redirected. This allows us to send them
    // along to that page after they login, which is a nicer user experience
    // than dropping them off on the home page.
    return <Navigate to="/login" state={{ from: location }} />;
  }

  return <Outlet />;
}

function App() {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(loadUser(null));
  }, [dispatch])
  
  return (
    <div>
    <BrowserRouter>
    <ToastContainer />
       <Routes>
       <Route path="/" element={<Login />} />
         <Route element={<RequireAuth />}>
           <Route path="/home" element= {<Home />} />
         </Route>
         <Route path="/login" element={<Login />} />
         <Route path="/register" element={<Register />} />
         <Route path="*" element={<NotFound />} />
       </Routes>
    </BrowserRouter>
    </div>
  );
}

export default App;
