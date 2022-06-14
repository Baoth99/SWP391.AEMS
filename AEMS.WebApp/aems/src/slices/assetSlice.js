import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import {uri} from './api'

const initialState = {
    items: [],
    name: "",
    email: "",
    phone: "",
    website: "",
    edit: false,
    status: null,
};

export const assetFetch = createAsyncThunk(
    "asset/assetFetch",
    async () => {
        try {
            const response = await axios.get(
                `${uri}`
            )
            return response.data
        }
        catch (error) {
            console.log(error);
        }
    }
)

export const assetUpdate = createAsyncThunk(
    "asset/assetUpdate",
    async ({id, data}) => {
        try {
            const response = await axios.put(`${uri}/${id}`, data)
            return response.data
        }
        catch (error) {
            console.log(error);
        }
    }
)

const assetSlice = createSlice({
    name: "asset",
    initialState,
    reducers: {
    },
    extraReducers: {
        [assetFetch.pending]: (state, action) => {
            state.status = "pending";
        },
        [assetFetch.fulfilled]: (state, action) => {
            state.items = action.payload;
            state.status = "success";
        },
        [assetFetch.rejected]: (state, action) => {
            state.status = "rejected";
        },
        [assetUpdate.pending]: (state, action) => {
            state.status = "pending";
        },
        [assetUpdate.fulfilled]: (state, action) => {
            state.items = action.payload;
            state.status = "success";
        },
        [assetUpdate.rejected]: (state, action) => {
            state.status = "rejected";
        },
    }
})

export default assetSlice.reducer