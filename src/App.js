import logo from "./logo.svg";
import "./App.css";
import { PowerBIEmbed } from "powerbi-client-react";
import { models } from "powerbi-client";
// import { useEffect } from "react";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <PowerBIEmbed
          embedConfig={{
            type: "report", // Supported types: report, dashboard, tile, visual and qna
            id: "6c1f2ddf-20ef-46e6-a710-ef2f39023a47",
            embedUrl:
              "https://app.powerbi.com/reportEmbed?reportId=6c1f2ddf-20ef-46e6-a710-ef2f39023a47&groupId=4a4b79d3-430d-40b8-a91e-4390ed0a4dd4&w=2&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly9XQUJJLVNPVVRILUVBU1QtQVNJQS1CLVBSSU1BUlktcmVkaXJlY3QuYW5hbHlzaXMud2luZG93cy5uZXQiLCJlbWJlZEZlYXR1cmVzIjp7Im1vZGVybkVtYmVkIjp0cnVlLCJhbmd1bGFyT25seVJlcG9ydEVtYmVkIjp0cnVlLCJjZXJ0aWZpZWRUZWxlbWV0cnlFbWJlZCI6dHJ1ZSwidXNhZ2VNZXRyaWNzVk5leHQiOnRydWUsInNraXBab25lUGF0Y2giOnRydWV9fQ%3d%3d",
            accessToken:
              "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImpTMVhvMU9XRGpfNTJ2YndHTmd2UU8yVnpNYyIsImtpZCI6ImpTMVhvMU9XRGpfNTJ2YndHTmd2UU8yVnpNYyJ9.eyJhdWQiOiIwMGEyMWY2NC1jZDYwLTQ0NDQtOGUwZi0wM2M5ZjRkMzY3NjIiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC8wNTcxODIyZC0zM2RjLTQ1NWUtYTA3Zi0xYjU1NzUyOTc1ODMvIiwiaWF0IjoxNjU1ODY4ODgwLCJuYmYiOjE2NTU4Njg4ODAsImV4cCI6MTY1NTg3NDA1OCwiYWNjdCI6MCwiYWNyIjoiMSIsImFpbyI6IkFTUUEyLzhUQUFBQTYyU2FNTnNpcXl3SDI3NEdjNDBtZXBNZFZzcmtrc1hsa3BHLzVkQjhtd009IiwiYW1yIjpbInB3ZCJdLCJhcHBpZCI6IjAwYTIxZjY0LWNkNjAtNDQ0NC04ZTBmLTAzYzlmNGQzNjc2MiIsImFwcGlkYWNyIjoiMCIsImF1dGhfdGltZSI6MTY1NTc4NzY0MCwiZW1haWwiOiJXaWxzb25AYmFvdGg3OTlvdXRsb29rLm9ubWljcm9zb2Z0LmNvbSIsImZhbWlseV9uYW1lIjoiTmljaG9sYXMiLCJnaXZlbl9uYW1lIjoiV2lsc29uIiwiaXBhZGRyIjoiMTE2LjEwOC4yMS43OCIsIm5hbWUiOiJXaWxzb24gTmljaG9sYXMiLCJvaWQiOiI5YTViYmQ4Mi0zYWYxLTQzMTQtYjNmYS1hYjM2OGRhYTAzMTciLCJyaCI6IjAuQVhFQUxZSnhCZHd6WGtXZ2Z4dFZkU2wxZzJRZm9nQmd6VVJFamc4RHlmVFRaMktIQUFBLiIsInNjcCI6IlVzZXJBY2Nlc3MiLCJzaWQiOiI0ZjcyMTIwYS01YjE4LTQ0NmQtYTBiOC1lZjUyYWE1YTEwZWIiLCJzdWIiOiJrLU5DLW9fNnllbXkxUkNGc3lXRHhHM2trTGlWM25xOFFOWEREWng0XzFVIiwidGVuYW50X2N0cnkiOiJWTiIsInRlbmFudF9yZWdpb25fc2NvcGUiOiJBUyIsInRpZCI6IjA1NzE4MjJkLTMzZGMtNDU1ZS1hMDdmLTFiNTU3NTI5NzU4MyIsInVuaXF1ZV9uYW1lIjoiV2lsc29uQGJhb3RoNzk5b3V0bG9vay5vbm1pY3Jvc29mdC5jb20iLCJ1cG4iOiJXaWxzb25AYmFvdGg3OTlvdXRsb29rLm9ubWljcm9zb2Z0LmNvbSIsInV0aSI6IlhDWXcxbHZZdDAtWndYZ3lEMTRXQUEiLCJ2ZXIiOiIxLjAifQ.NrBt9esRCKgAIXqmDCwNeQ_W1v52bYlo4PkhSL44cNI88bqYlB6TzBJykekVLHbUFr_EjD7THMUnQOBmTtN4hCN9o4e4VIWz4me0KzmRN0O1rsZOxIbzQvswKAluyBStaVY_YhJMnLYu6nbIMwm-9Xsfn2IwDvEFHE9AlO7SaVL1xO0WhfgiOjcLW25zTRWfgafuOb5mVt8mqaUhLSIr_EEaTMfC0dzWkJ9hCXaDVb-Aa1nUgt8w1d2fMzBJsi8tZ-SLTarQxkmh23zWYoJKfmSyJ23HjPtoKMMgoK-lhXrWaAxKDM4zUQMgVrT6ejJnaujbMK4pu42oeUhlS-L1UQ",
            // "H4sIAAAAAAAEAB2Ut66EVgAF_-W1WFoyXEsuyDlnOmCBJeclWP53P7uf4mg0On__2NndT9n758-fygDTOIgj8whuF9Y0JdDViPTa2qNOVaJzWVB5CmarK1B7F2Lo3ZcDU8sfS1aE_TM3tDKv6qVdHmnQJn8o3tVyyzxgFRXxnW6F2ZexBGUbV_exmBZwKJqBpcWSrp_0x9nfd4rjXeGhnq9_GBWJv8EmOLagi0OCG5qq9dnRZhLFk9bGyCwTTgH2DTrmLjlBrxyt9UqgI-U2j5xR82pGFPKgORoSjLhOKAqnnboNiXRtIWOi8DDUovpMghW2GWwNYiXIjhkmd8ECSiymtmjiHMA0sSZt3qFNtUvatDFAoRXlBlL9pH0DXggHgzkxCFLfEzHaHSuxEXJ-rFeLmD0osFNXxSz4fGGQWsNen2H5mceQIaajobSYXN6F_Epac8ASSl2Gpn15-0e_1b6thhoM54F6ZQ6wcdFel96o6CZApZNfeBrWOffS1V3y7DdLE9WbV4jH0JC5fz6-NHbmENaV5eZTbTg4iVtl7oF5s7U7Sb_iQoK5m05tf9j2bLoMdqR0WxHzQEhcHZCEhfxH7N-EQWER6jH4cHgWzhCA3RiWx2XB-m4qjCZQ8DaJzUvJuFwYLZwXRx3DmmiTy8xjiifWCxbpeG77kOAM-IWoRTMvpA_VNLExrsnHrUGH3bhUKsdSPTBgkcrfFt-xaeeqpLzF6ZjIEml3C0Vx9OY7tvbiurC8YvQsLADkqq-uIMlnaH1EoRefmPPPbTzQe8nGQHg0V9nhaMedqoWJ0uCVMbjJSeKJO8yLv37--OHWe94nrbx_048PjM9PT_KMeFoQ8tQ-LJXsJ42huRmwlItBhCLJ3CIc77IFOj7FwbLlsoF9iSLvrdQRabF5kKfV7DbysjQTWA4SI3AfQtARjNZoX52Z20VVh2GuLo441PXhJfTtrJ6Ukr9jv4FwBae-vx_6a0U2X0n4kj-fV_ZJJHmQQdAndeSkxzo-_XjBkXSlN2lSh0dNt-veGQTsBbA9uwazzj63j7Y2dzLajbCo4I4crUVTPGBH7152SVnKnXw_mJCblxDw63JlyQM8OueMreTStmnWJ63t1hlorCnkuyZDGmg-nvJH_KVKKItzasjouj_0t3tfrNMivYmCa3NdRcGmqipZy8XS-q__Nd_zp1yV8NeyS1xDwq2KoVnJl76dU26xxvmf8pp6zPZjLX-x_Ft86kAq5QolP_AsJGkkrzRAluno_L0OMMxXNIEJi0DELjNOu0hMp3DirgPiRMjWJlTxI0C4L-z5IiBqLDxeNuaU9Vwl_WPbj2be-zuh3NVcz2G57JZ6V-_5qgXU2xZqQI8FUIgAnCRAHhzGWm7bVp7repisKgAL8qd2i1j2Mfsyyfr2tjc6DDQZiK1aBBK5t98oE2yci5w1C9zDkmEz8t4WMeqMCd3vvpLSQG0bAoJhPJ3rE_I6j9n4GW483vdXcKrr3ohHAPPErm2_zxOdN0dqwg0tBJg6Pd0kpEYh4WaSKEKzfVzb-vMQrvTywirpmBBDsAU3ilNbNH-B6op9tOw_zf_8Cz0ft9nuBQAA.eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly9XQUJJLVNPVVRILUVBU1QtQVNJQS1CLVBSSU1BUlktcmVkaXJlY3QuYW5hbHlzaXMud2luZG93cy5uZXQiLCJlbWJlZEZlYXR1cmVzIjp7Im1vZGVybkVtYmVkIjpmYWxzZX19",
            tokenType: models.TokenType.Aad,
            settings: {
              panes: {
                filters: {
                  expanded: false,
                  visible: false,
                },
              },
              background: models.BackgroundType.Transparent,
            },
          }}
          eventHandlers={
            new Map([
              [
                "loaded",
                function () {
                  console.log("Report loaded");
                },
              ],
              [
                "rendered",
                function () {
                  console.log("Report rendered");
                },
              ],
              [
                "error",
                function (event) {
                  console.log(event.detail);
                },
              ],
            ])
          }
          cssClassName={"device-style-class"}
          getEmbeddedComponent={(embeddedReport) => {
            window.report = embeddedReport;
          }}
        />
      </header>
    </div>
  );
}

export default App;
