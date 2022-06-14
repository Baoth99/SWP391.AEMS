import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const assetApi = createApi({
    reducerPath: "assetApi",
    baseQuery: fetchBaseQuery({baseUrl: "http://localhost:5000/"}),
    endpoints: (builder) => ({
        getAllAssets: builder.query({
            query: () => `products`
        })
    })
})

export const {useGetAllAssetsQuery} = assetApi