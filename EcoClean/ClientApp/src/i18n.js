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

                    Sort: "Sort",
                    "Create a new report": "Create a new report",

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

                    Statistics: "Statistics",
                    Rate: "Rate",

                    "Enterprise Name": "Enterprise Name",
                    "Air Pollution Substance": "Air Pollution Substance",
                    "Water Pollution Substance": "Water Pollution Substance",
                    "Air Emissions": "Air Emissions",
                    "Water Emissions": "Water Emissions",
                    "Tax": "Tax",
                    "Comment": "Comment",
                    "Date": "Date",

                    "Certificate Date": "Certificate Date",

                    "Add new data": "Add new data",
                    
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

                    Sort: "Сортувати",
                    "Create a new report": "Сформувати новий звіт",

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
                    "Enterprises": Підприємства,*/

                    Statistics: "Статистика",
                    Rate: "Рейтинг",

                    "Enterprise Name": "Назва підприємства",
                    "Air Pollution Substance": "Речовина забруднення повітря",
                    "Water Pollution Substance": "Речовина забруднення води",
                    "Air Emissions": "Об'єм викидів у повітря",
                    "Water Emissions": "Об'єм викидів у воду",
                    "Tax": "Податок",
                    "Comment": "Коментар",
                    "Date": "Дата",

                    "Certificate Date": "Дата отримання сертифікату",

                    "Add new data": "Додати нові дані",

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