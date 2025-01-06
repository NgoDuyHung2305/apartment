// store.ts
import { legacy_createStore } from 'redux';

interface UserState {
  user: string;
}

const initialState: UserState = {
  user: 'Người mặc định',
};

// Action type
interface UpdateUserAction {
  type: 'UPDATE_USER';
  payload: string;
}

type UserAction = UpdateUserAction;

const userReducer = (state = initialState, action: UserAction): UserState => {
  switch (action.type) {
    case 'UPDATE_USER':
      return { ...state, user: action.payload };
    default:
      return state;
  }
};

const store = legacy_createStore(userReducer);

export default store;
