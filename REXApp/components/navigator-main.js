import React, { Component } from 'react';
import ReactNative from 'react-native';
import ContactDetail from '../components/contact-detail';
import ListContact from '../components/list-contacts';
import { Scene, Router } from 'react-native-router-flux';

import SceneContactDetail from '../scene/scene-contact-detail';
import SceneMain from '../scene/scene-main';

import TabIcon from '../components/tab-icon';
import { Actions, ActionConst } from 'react-native-router-flux';
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
                <Scene key="contactDetail" tabBarStyle={styles.tabBarStyle} tabs={true} hideNavBar={false} title="Contact details">
                    <Scene
                        key="tabBasicInfo"
                        icon={TabIcon}
                        title="Tin đặt hàng" 
                        onSelect={() => {
                            Actions.tabInfo({ type: ActionConst.REFRESH });
                        } }
                        >
                        <Scene key="tabInfo" title="Tin đặt hàng" component={ContactDetail} hideNavBar={false} />
                    </Scene>
                    <Scene
                        key="tabSummary"
                        title="Thống kê"
                        icon={TabIcon}
                        onSelect={() => {
                            Actions.tabSum({ type: ActionConst.REFRESH });
                        } }
                        >
                        <Scene key="tabSum" title="Thống kê"  component={SceneContactDetail} hideNavBar={false} />
                    </Scene>
                </Scene>
            </Scene>
        </Router>
    }
}

const styles = StyleSheet.create({
    navi: {
        width: 100,
    },
    tabBarStyle: {
        borderTopWidth: .5,
        borderColor: '#b7b7b7',
        backgroundColor: 'grey',
        opacity: 0.75
    }
});

export default NavigatorMain;