import React, { Component, useState } from 'react';
import { withTranslation } from 'react-i18next';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row } from 'reactstrap';
import ReportsApi from '../../services/ReportsApi';
import authService from '../../api-authorization/AuthorizeService';


class AddReport extends Component {

    constructor() {
        super();

        this.addReportHandler = this.addReportHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

        this.state = {
            enterprises: [],
            selectedEnterprise: "",
            validationError: ""
        };
    }

    async componentDidMount() {
        document.title = "Add Report";

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


    addReportHandler = (report, callback) => ReportsApi.createReport(report, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            enterpriseId: this.state.selectedEnterprise,
            comment: event.target.elements['comment'].value
        };

        this.addReportHandler(data, () => this.props.history.push('/reports'));
    }

    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>{t("Add Report")}</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="enterpriseId">{t("Enterprise Name")}</Label>
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
                                            <Label htmlFor="comment">Comment</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="comment" placeholder="Comment" />
                                        </Col>
                                    </FormGroup>
                                    
                                    <Button type="submit" color="primary">{t("Submit")}</Button>
                                </form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}


export default withTranslation()(AddReport);
