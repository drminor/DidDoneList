import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class First extends React.Component<RouteComponentProps<{}>, {}> {

    public render() {
        return <div>
            <h1>Hello, world!</h1>
            <CounterButton />
            <Input/>
        </div>;
    }
}

class CounterButton extends React.Component {


    state = { count: 0 }

    handleClick = () => {
        const { count } = this.state

        this.setState({ count: count + 1 })
    }

    render() {
        const { count } = this.state

        return (
            <button type='button' onClick={this.handleClick}>
                Click HERE to increment: {count}
            </button>
        )
    }
}


class Input extends React.Component<any,any> {

    state = { value: 0 }

    handleChange = (e:any) => {
        this.setState({ value: e.target.value })
    }

    render() {
        const { value } = this.state

        return (
            <div>
                <label htmlFor={'id'}>
                    Enter value
        </label>
                <input
                    id={'id'}
                    type={'text'}
                    value={value}
                    placeholder={'Placeholder'}
                    onChange={this.handleChange}
                />
                <br />
                <br />
                My value: {value}
            </div>
        )
    }
}
