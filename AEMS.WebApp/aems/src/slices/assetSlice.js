import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";

const initialState = {
    items: [],
    status: null,
};

export const assetFetch = createAsyncThunk(
    "asset/assetFetch",
    async () => {
        try {
            const response = await axios.get(
                "https://chaoo-online-shop.herokuapp.com/products"
            )
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
    reducers: {},
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
    }
})

export default assetSlice.reducer