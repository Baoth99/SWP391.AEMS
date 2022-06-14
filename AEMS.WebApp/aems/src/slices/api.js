export const uri = 'https://jsonplaceholder.typicode.com/users'

export const setHeaders = () => {
   const headers = {
       headers: {
           'x-auth-token': localStorage.getItem("token")
       }
   }
   return headers
}