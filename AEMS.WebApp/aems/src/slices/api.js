<<<<<<< HEAD
export const uri = 'https://jsonplaceholder.typicode.com/users'
=======
export const uri = 'http://localhost:5000/api'
>>>>>>> dc89784e4e15922c837e0beaeac02034fce51c24

export const setHeaders = () => {
   const headers = {
       headers: {
           'x-auth-token': localStorage.getItem("token")
       }
   }
   return headers
}