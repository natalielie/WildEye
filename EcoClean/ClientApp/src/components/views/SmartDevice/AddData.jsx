import React, { Component, useState } from 'react';
import { withTranslation } from 'react-i18next';
import authService from '../../api-authorization/AuthorizeService';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row } from 'reactstrap';
import SmartDeviceApi from '../../services/SmartDeviceApi';
import '../../../custom.css';

class AddData extends Component {

    constructor() {
    super();

    this.addDataHandler = this.addDataHandler.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);

    this.state = {
        enterprises: [],
        selectedEnterprise: "",
        validationError: ""
    };
}
   

    async componentDidMount() {

        document.title = "Add Data";

        const token = await authService.getAccessToken();

        fetch(
            'api/client/getAllEnterprises', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
            .then(response => {
                return response.json();
            })
            .then(data => {
                let enterprisesFromApi = data.map(enterprise => {
                    return { value: enterprise.enterpriseId, display: enterprise.name };
                });
                this.setState({
                    enterprises: [
                        {
                            value: "",
                            display:
                                "(Select your enterprise)"
                        }
                    ].concat(enterprisesFromApi)
                });
            })
            .catch(error => {
                console.log(error);
            });
    }

    addDataHandler = (data, callback) => SmartDeviceApi.addSmartDeviceData(data, callback);



    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            enterpriseId: parseFloat(this.state.selectedEnterprise),
            airPollution: parseFloat(event.target.elements['airPollution'].value),
            waterPollution: parseFloat(event.target.elements['waterPollution'].value),
        };

        this.addDataHandler(data, () => this.props.history.push('/smart-device'));
    }

    render() {
        const { t } = this.props;
        
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>{t("Add Pollution Data")}</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="enterprise">{t("Enterprise")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">

                                            <div>
                                                <select
                                                    value={this.state.selectedEnterprise}
                                                    onChange={e =>
                                                        this.setState({
                                                            selectedEnterprise: e.target.value,
                                                            validationError:
                                                                e.target.value === ""
                                                                    ? "You must select your enterprise"
                                                                    : ""
                                                        })
                                                    }
                                                
                                                >
                                                    {this.state.enterprises.map(enterprise => (
                                                        <option
                                                            key={enterprise.value}
                                                            value={enterprise.value}
                                                        >
                                                            {enterprise.display}
                                                        </option>
                                                    ))}
                                                </select>
                                                <div
                                                    style={{
                                                        color: "red",
                                                        marginTop: "5px"
                                                    }}
                                                >
                                                    {this.state.validationError}
                                                </div>
                                            </div>


                                        </Col>
                                    </FormGroup>

                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="airPollution">{t("Air Pollution")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="airPollution" placeholder="Air pollution (ppm)" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="waterPollution">{t("Water Pollution")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="waterPollution" placeholder="Water pollution (mg/l)" />
                                        </Col>
                                    </FormGroup>

                                    
                                    <Button class="btn btn-primary" style={{ marginTop: 20 }} type="submit" color="primary">{t("Submit")}</Button>
                                </form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}


export default withTranslation()(AddData);
