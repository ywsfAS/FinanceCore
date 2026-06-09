import Input from "../Input/Input";
import styles from "./TransactionCreatePopUp.module.css";
import {useForm} from 'react-hook-form';

const TransactionCreatePopUp = () => {
    return (
        <div className={styles.overlay}>
            <form className={styles.popUp}>
                <h1 className={styles.title}>Create a nw transaction</h1>
               



            </form>





        </div>
    )


};
export default TransactionCreatePopUp;
