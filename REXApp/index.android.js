/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 * @flow
 */
import React, { Component } from 'react';
import ReactNative from 'react-native';
import DatePicker from './controls/date-picker.android.js';
import SceneContactDetail from './scene/scene-contact-detail';
import ListContact from './components/list-contacts';

import {
  AppRegistry,
  StyleSheet,
  View,
  Picker,
  Navigator
} from 'react-native';

export default class REXApp extends Component {
  
  render() {
    const routes = [
    {title: 'Contact List', index: 0},
    {title: 'Contact Detail', index: 1},
    ];
    return (
       <Navigator
      initialRoute={routes[0]}
      initialRouteStack={routes}
      renderScene={(route, navigator) => {
        return route.index == 0 ? 
        <ListContact title={route.title} onForward={(contact) => {   
          debugger; 
              const nextIndex = route.index + 1;
              navigator.push({
                title: 'Scene ' + nextIndex,
                index: nextIndex,
                passProps:contact
              });
            }} />
            :
            <SceneContactDetail contact={route.passProps}/>
      }}
    />
    );
  }
}

const styles = StyleSheet.create({
  picker: {
    width: 100,
  }
});

AppRegistry.registerComponent('REXApp', () => REXApp);
