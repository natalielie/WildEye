import React, { Component } from 'react';
import { withTranslation } from 'react-i18next';
import eco from '../img/eco.png';
import "../custom.css";


export class Home extends Component {
    static displayName = Home.name;

    render() {
        const { t } = this.props;
        return (
            <div>
                <h1>{t("This is Ecoprise")}</h1>
                <p>{t("The environment-friendly system, which provides the necessary and proper management of your enterprise.")}</p>
                <img className="eco_logo" src={eco} alt={"think_green"} />
            </div>
        );
    }
}

export default withTranslation()(Home);