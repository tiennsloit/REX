import React, { Component } from 'react';
import ReactNative from 'react-native';
import ContactDetail from '../components/contact-detail';
import ListContact from '../components/list-contacts';
import ListOrders from '../components/list-orders';
import OrderDetail from '../components/order-detail';
import { Scene, Router, Reducer } from 'react-native-router-flux';

import SceneContactDetail from '../scene/scene-contact-detail';
import SceneMain from '../scene/scene-main';

import TabIcon from '../components/tab-icon';
import { Actions, ActionConst } from 'react-native-router-flux';
import { PUSH_ACTION, RESET_ACTION, REFRESH_ACTION } from 'react-native-router-flux/src/Actions';
import {
    StyleSheet,
    View,
    Text,

    Navigator,
    TouchableHighlight
} from 'react-native';

const reducerCreate = params=>{
    const defaultReducer = Reducer(params);
    return (state, action)=>{
        console.log("ACTION:", action);
        return defaultReducer(state, action);
    }
};

class NavigatorMain extends Component {
    constructor(props)
        {
            super(props);
           
        }
    render() {
        return <Router createReducer={reducerCreate} >
            <Scene key="root"  >
                <Scene key="contactList"  hideNavBar={false} component={ListContact} title="List of contacts" />
                <Scene key="contactDetail" tabBarStyle={styles.tabBarStyle} tabs={true} hideNavBar={false} title="Contact details" >
                    <Scene
                        key="tabOrders"
                        title="Orders"
                        icon={TabIcon}
                        navigationBarStyle={styles.navi}
                        hideNavBar={false}
                        onSelect={(attr) => {
                            Actions.tabOrders({type:ActionConst.REFRESH});
                        } }
                        >
                        <Scene key="tabOrs" title="Orders"  component={ListOrders} hideNavBar={false} >
                              
                        </Scene>
                        <Scene key="orderDetail" title="OD"  hideNavBar={false} >
                            <Scene key="od" title="Order Detail"  component={OrderDetail} hideNavBar={false} />
                        </Scene>  
                    </Scene>
                    <Scene
                        key="tabBasicInfo"
                        icon={TabIcon}
                        title="Contact" 
                        navigationBarStyle={styles.navi}
                        onSelect={(props) => {
                            Actions.tabInfo({ type: ActionConst.REFRESH });
                        } }
                        >
                        <Scene key="tabInfo" title="Contact" component={ContactDetail} hideNavBar={false} />
                    </Scene>
                    <Scene
                        key="tabSummary"
                        title="Summary"
                        icon={TabIcon}
                        navigationBarStyle={styles.navi}
                        onSelect={() => {
                            Actions.tabSum({ type: ActionConst.REFRESH });
                        } }
                        >
                        <Scene key="tabSum" title="Summary"  component={SceneContactDetail} hideNavBar={false} />
                    </Scene>
                    
                    
                </Scene>
                
            </Scene>
        </Router>
    }
}

const styles = StyleSheet.create({
    navi: {
        backgroundColor:'grey',
        opacity: 0.5
    },
    tabBarStyle: {
        borderTopWidth: .5,
        borderColor: '#b7b7b7',
        backgroundColor: 'grey',
        opacity: 0.75
    }
});

export default NavigatorMain;