import React, { useState } from 'react';

import { useHistory } from "react-router-dom";
//import i18n from "../../i18n";
import { useTranslation } from 'react-i18next';


const StatisticsSwitcher = props => {
    const history = useHistory();
    const { t, i18n } = useTranslation();

    const [stat, setStat] = useState({
        data: {
            current_stat: 'desc',
        }
    });

    const {
        current_stat
    } = stat.data;


    const onChange = e => {
        const { name, value } = e.target;
        const { data } = stat;
        setStat({
            data: {
                ...data,
                [name]: value
            }
        });
    }


    const statOptions = [
        {
            label: "Descending",
            value: "desc",
        },
        {
            label: "Ascending",
            value: "asc",
        },
    ];

    return (
        <>
            <h5>{t("Sort")}</h5>
            <div className="form-group">
                <select value={current_stat} onChange={onChange} id="license_type" name="current_stat">
                    {statOptions.map((option) => (
                        <option value={option.value}>{option.label}</option>
                    ))}
                </select>
            </div>
        </>
    )
}

export default StatisticsSwitcher;