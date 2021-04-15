import i18next from "i18next";
import { initReactI18next } from "react-i18next";
import HttpApi from "i18next-http-backend";

import { withTranslation } from 'react-i18next';


import i18n from "i18next";

i18n
    .use(initReactI18next)
    .init({
        resources: {
            en: {
                translations: {

                    Selectlanguage: "Select language",

                    "Ecoprise": "Ecoprise",
                    "This is Ecoprise": "This is Ecoprise",
                    "The environment-friendly system, which provides the necessary and proper management of your enterprise.":
                        "The environment-friendly system, which provides the necessary and proper management of your enterprise.",

                    Home: "Home",
                    Name: "Name",
                    Edit: "Edit",
                    Delete: "Delete",
                    Workplace: "Workplace",

                    "All Enterprises": "All Enterprises",
                    "My Enterprises": "My Enterprises",
                    Statistics: "Statistics",
                    "Reports": "Reports",
                    "Certificates": "Certificates",
                    "Smart Device Data": "Smart Device Data",
                    Taxes: "Taxes",
                    
                    Kind: "Kind",

                    Date: "Date",
                    "Begin Date": "Begin Date",
                    "End Date": "End Date",

                    Terminate: "Terminate",

                    
                    Submit: "Submit",
                    "Date and time": "Date and time",
                    "Add Dates": "Add Dates",

                    Hello: "Hello",
                    Register: "Register",
                    Login: "Login",
                    Logout: "Logout",

                    Confirmation: "Confirmation",
                    Cancel: "Cancel",
                    "Are you sure?": "Are you sure?",


                    "Add a new enterprise": "Add a new enterprise",
                    /*"EnterpriseId": EnterpriseId,
                    "Enterprise Name": "Enterprise Name",
                    "Enterprises": Enterprises,*/

                    
                },
            },
            ua: {
                translations: {
                    Selectlanguage: "Оберіть мову",

                    "Ecoprise": "Ecoprise",
                    "This is Ecoprise": "Це Ecoprise",
                    "The environment-friendly system, which provides the necessary and proper management of your enterprise.":
                        "Екологічно безпечна система, що надає необхідний і правильний функціонал для керування вашим підприємством",

                    Home: "Головна",
                    Name: "Ім'я",
                    Edit: "Відредагувати",
                    Delete: "Видалити",
                    Workplace: "Місце роботи",

                    "All Enterprises": "Підприємства",
                    "My Enterprises": "Мої підприємства",
                    Statistics: "Статистика",
                    "Reports": "Звіти",
                    "Certificates": "Сертифікати",
                    "Smart Device Data": "Смарт-Пристрій",
                    Taxes: "Податки",

                    Kind: "Вид",
                    
                   
                    Date: "Дата",
                    
                    "Begin Date": "Початок",
                    "End Date": "Кінець",

                   
                    Terminate: "Розірвати договір",

                    
                    Submit: "Підтвердити",
                    "Date and time": "Дата та час",
                    "Add Dates": "Додати дати",

                    Hello: "Привіт,",
                    Register: "Зареєструватися",
                    Login: "Увійти",
                    Logout: "Вийти",

                    Confirmation: "Підтвердження",
                    Cancel: "Відмінити",
                    "Are you sure?": "Ви впевнені?",


                    "Add a new enterprise": "Додати нове підприємство",
                   /* "EnterpriseId": EnterpriseId,
                    "Enterprise Name": "Назва підприємства",
                    "Enterprises": Підприємства,*/

                },
            },
        },

        fallbackLng: "en",
        debug: true,

        // have a common namespace used around the full app
        ns: ["translations"],
        defaultNS: "translations",

        keySeparator: false, // we use content as keys

        interpolation: {
            escapeValue: false, // not needed for react!!
            formatSeparator: ","
        },

        react: {
            wait: true
        }
    });

export default i18n;