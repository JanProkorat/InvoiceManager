import React, {useEffect, useState} from "react";
import {IInvoice, InvoiceState} from "./InvoiceScene";

interface IInvoiceDetailTemplateProps {
    detail: IInvoice | null;
    onAddInvoiceItemClick: (amount: number, receiver: string, deadline: string, invoiceId: number) => void;
    onDeleteInvoiceItemClicked: (invoiceItemId: number) => void;
    onUpdateInvoiceClicked: (invoiceId: number, invoiceName: string) => void;
    onPayInvoiceClicked: (invoiceId: number) => void;
}

export const InvoiceDetailTemplate = (props: IInvoiceDetailTemplateProps) => {

    const [detailToDisplay, setDetailToDisplay] = useState<IInvoice | null>(null);
    const [receiver, setReceiver] = useState<string>("");
    const [amount, setAmount] = useState<number>(0);
    const [deadline, setDeadline] = useState<string>("");
    const [invoiceName, setInvoiceName] = useState<string>("");

    useEffect(() => {
        setDetailToDisplay(props.detail);
    }, [props.detail])

    useEffect(() => {
        if (detailToDisplay !== null) {
            setInvoiceName(detailToDisplay.invoiceName);
        }
    }, [detailToDisplay])

    return (
        <div className="w-100">
            {detailToDisplay !== null && <h4>{"Detail faktury " + detailToDisplay.invoiceName}</h4>}
            {detailToDisplay &&
            <div className="w-100">
                <div>
                    <div className="d-flex justify-content-between">
                        <label>Název faktury</label>
                        <input type="text" value={invoiceName} onChange={e => setInvoiceName(e.target.value)}/>
                        <button
                            type="button" disabled={invoiceName === "" || invoiceName === detailToDisplay.invoiceName}
                            onClick={() => props.onUpdateInvoiceClicked(detailToDisplay!.id, invoiceName)}
                        >
                            Upravit fakturu
                        </button>
                    </div>
                    <div className="d-flex justify-content-between">
                        <label>{"Stav: " + InvoiceState[detailToDisplay.state]}</label>
                        <button
                            type="button"
                            onClick={() => props.onPayInvoiceClicked(detailToDisplay!.id)}
                        >
                            Zaplatit fakturu
                        </button>
                    </div>
                </div>
                <h5>Položky faktury</h5>
                <div className="w-100 d-flex justify-content-between mt-2">
                    <div>
                        <div className="d-flex justify-content-between">
                            <label>Přidat částku k zaplacení</label>
                            <input type="number" value={amount} onChange={e => setAmount(Number(e.target.value))}/>
                        </div>
                        <div className="d-flex justify-content-between">
                            <label>Přidat příjemce</label>
                            <input type="text" value={receiver} onChange={e => setReceiver(e.target.value)}/>
                        </div>
                        <div className="d-flex justify-content-between">
                            <label>Přidat deadline</label>
                            <input type="date" value={deadline} onChange={e => setDeadline(e.target.value)}/>

                        </div>
                    </div>
                    <button
                        type="button" disabled={amount === 0 || receiver === "" || deadline === ""}
                        onClick={() => {
                            props.onAddInvoiceItemClick(amount, receiver, deadline, detailToDisplay!.id);
                            setDeadline("");
                            setReceiver("");
                            setAmount(0);
                        }}
                    >
                        Přidat položku
                    </button>
                </div>
                {detailToDisplay.items.length === 0 ?
                    <label className="mt-2">Faktura neobsahuje žádné položky</label> :
                    <table className="mt-2">
                        <thead>
                        <tr>
                            <th>Částka k zaplacení</th>
                            <th>Příjemce</th>
                            <th>Deadline</th>
                            <th/>
                        </tr>
                        </thead>
                        <tbody>
                        {detailToDisplay.items.map((obj) => (
                            <tr>
                                <td>{obj.amountToPay}</td>
                                <td>{obj.receiver}</td>
                                <td>{obj.deadline}</td>
                                <td>
                                    <button
                                        type="button"
                                        onClick={() => props.onDeleteInvoiceItemClicked(obj.id)}
                                    >
                                        Odstranit položku
                                    </button>
                                </td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                }
            </div>
            }
        </div>
    )
}