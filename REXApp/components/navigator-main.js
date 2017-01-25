import React, { Component } from 'react';
import ReactNative from 'react-native';
import ContactDetail from '../components/contact-detail';
import ListContact from '../components/list-contacts';
import { Scene, Router } from 'react-native-router-flux';
import {
    StyleSheet,
    View,
    Text,
    Navigator,
    TouchableHighlight
} from 'react-native';

class NavigatorMain extends Component {
    render() {

        return <Router>
            <Scene key="root">
                <Scene key="contactList" hideNavBar={true} component={ListContact} title="List of contacts" />
                <Scene key="contactDetail" hideNavBar={false} component={ContactDetail} title="Contact details" />
            </Scene>
        </Router>
    }
}

const styles = StyleSheet.create({
    navi: {
        width: 100,
    }
});

export default NavigatorMain;