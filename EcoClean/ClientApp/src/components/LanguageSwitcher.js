import React, { useState } from 'react';

import { useHistory } from "react-router-dom";
//import i18n from "../../i18n";
import { useTranslation } from 'react-i18next';
import '../custom.css';

const LanguageSwitcher = props => {
    const history = useHistory();
    const { t, i18n } = useTranslation();

    const [language, setLanuage] = useState({
        data: {
            current_language: 'en',
        }
    });

    const {
        current_language
    } = language.data;


    const onChange = e => {
        const { name, value } = e.target;
        const { data } = language;
        setLanuage({
            data: {
                ...data,
                [name]: value
            }
        });
        i18n.changeLanguage(value);
    }


    const languageOptions = [
        {
            label: "en",
            value: "en",
        },
        {
            label: "укр",
            value: "ua",
        },
    ];

    return (
        <>
            <div className="form-group" >
                <select className="langSelect" value={current_language} onChange={onChange} id="license_type" name="current_language">
                    {languageOptions.map((option) => (
                        <option value={option.value}>{option.label}</option>
                    ))}
                </select>
            </div>
        </>
    )
}

export default LanguageSwitcher;