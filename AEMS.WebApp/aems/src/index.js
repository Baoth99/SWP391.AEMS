import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { Provider } from "react-redux";
import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import assetReducer, { assetFetch } from "./slices/assetSlice";
import {assetApi} from "./slices/assetApi"

const store = configureStore({
  reducer: {
    asset: assetReducer,
    auth: authReducer,
    [assetApi.reducerPath]: assetApi.reducer
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(assetApi.middleware)
})

store.dispatch(assetFetch())

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
   <Provider store={store}>
     <App />
   </Provider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
