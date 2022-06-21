export const uri = 'http://elbaems-env.eba-knxsy2an.ap-southeast-1.elasticbeanstalk.com/api/v1/device'
export const loginUrl = "http://localhost:5000/api";


export const setHeaders = () => {
   const headers = {
       headers: {
           'x-auth-token': localStorage.getItem("token")
       }
   }
   return headers
}
