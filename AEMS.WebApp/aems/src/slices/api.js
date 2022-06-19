export const uri = 'https://jsonplaceholder.typicode.com/users'
export const loginUrl = "http://localhost:5000/api";


export const setHeaders = () => {
   const headers = {
       headers: {
           'x-auth-token': localStorage.getItem("token")
       }
   }
   return headers
}