import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import jwtDecode from "jwt-decode";
import axios from "axios";
import { uri, loginUrl } from "./api";

const initialState = {
    token: localStorage.getItem("token"),
    name: "",
    email: "",
    _id: "",
    registerStatus: "",
    registerError: "",
    loginStatus: "",
    loginError: "",
    userLoaded: false,
}

export const msalConfig = {
  auth: {
    clientId: "cb584841-262a-4219-b141-0983c26e4f15",
    authority: "https://login.microsoftonline.com/447080b4-b9c6-4b0b-92fd-b543a68b4e97", // This is a URL (e.g. https://login.microsoftonline.com/{your tenant ID})
    redirectUri: "http://localhost:3000",
  },
  cache: {
    cacheLocation: "sessionStorage", // This configures where your cache will be stored
    storeAuthStateInCookie: false, // Set this to "true" if you are having issues on IE11 or Edge
  }
};

// Add scopes here for ID token to be used at Microsoft identity platform endpoints.
export const loginRequest = {
 scopes: ["User.Read"]
};

// Add the endpoints here for Microsoft Graph API services you'd like to use.
export const graphConfig = {
    graphMeEndpoint: "https://graph.microsoft.com"
};

export const registerUser = createAsyncThunk(
    "auth/registerUser",
    async (values, { rejectWithValue }) => {
        try {
            const token = await axios.post(`${uri}/register`, {
                name: values.name,
                email: values.email,
                password: values.password
            })
            localStorage.setItem("token", token.data)
            return token.data
        }
        catch (error) {
            console.log(error.response.data);
            return rejectWithValue(error.response.data)
        }
    }
)

export const loginUser = createAsyncThunk(
    "auth/loginUser",
    async (values, { rejectWithValue }) => {
        try {
            const token = await axios.post(`${loginUrl}/login`, {
                email: values.email,
                password: values.password
            })
            localStorage.setItem("token", token.data)
            return token.data
        }
        catch (error) {
            console.log(error.response.data)
            return rejectWithValue(error.response.data)
        }
    }
)

export const getUser = createAsyncThunk(
    "auth/getUser",
    async (id, { rejectWithValue }) => {
        try {
            const token = await axios.get(`${uri}/user/${id}, setHeaders()`)
            localStorage.setItem("token", token.data)
            return token.data
        }
        catch (error) {
            console.log(error.response.data)
            return rejectWithValue(error.response.data)
        }
    }
)

const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        loadUser(state, action) {
            const token = state.token
            if (token) {
                const user = jwtDecode(token)
                return {
                    ...state,
                    token,
                    name: user.name,
                    email: user.email,
                    _id: user._id,
                    userLoaded: true
                }
            }
            else return { ...state, userLoaded: true }
        },
        logoutUser(state, action) {
            localStorage.removeItem("token")
            return {
                ...state,
                token: "",
                name: "",
                email: "",
                _id: "",
                registerStatus: "",
                registerError: "",
                loginStatus: "",
                loginError: "",
            }
        }
    },
    extraReducers: (builder) => {
        builder.addCase(registerUser.pending, (state, action) => {
            return {...state, registerStatus: "pending"}
        })
        builder.addCase(registerUser.fulfilled, (state, action) => {
            if (action.payload) {
              const user = jwtDecode(action.payload);
              return {
                ...state,
                token: action.payload,
                name: user.name,
                email: user.email,
                _id: user._id,
                registerStatus: "success",
              };
            } else return state;
          });
          builder.addCase(registerUser.rejected, (state, action) => {
            return {
              ...state,
              registerStatus: "rejected",
              registerError: action.payload,
            };
          });
          builder.addCase(loginUser.pending, (state, action) => {
            return { ...state, loginStatus: "pending" };
          });
          builder.addCase(loginUser.fulfilled, (state, action) => {
            if (action.payload) {
              const user = jwtDecode(action.payload);
              return {
                ...state,
                token: action.payload,
                name: user.name,
                email: user.email,
                _id: user._id,
                loginStatus: "success",
              };
            } else return state;
          });
          builder.addCase(loginUser.rejected, (state, action) => {
            return {
              ...state,
              loginStatus: "rejected",
              loginError: action.payload,
            };
          });
          builder.addCase(getUser.pending, (state, action) => {
            return {
              ...state,
              getUserStatus: "pending",
            };
          });
          builder.addCase(getUser.fulfilled, (state, action) => {
            if (action.payload) {
              const user = jwtDecode(action.payload);
              return {
                ...state,
                token: action.payload,
                name: user.name,
                email: user.email,
                _id: user._id,
                getUserStatus: "success",
              };
            } else return state;
          });
          builder.addCase(getUser.rejected, (state, action) => {
            return {
              ...state,
              getUserStatus: "rejected",
              getUserError: action.payload,
            };
          });
        },
      });
      
      export const { loadUser, logoutUser } = authSlice.actions;
      
      export default authSlice.reducer;