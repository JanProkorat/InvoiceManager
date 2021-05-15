import {IInvoice, InvoiceState} from "./InvoiceScene";
import React, {useEffect, useState} from "react";
import '../custom.css'


interface IInvoiceListTemplateProps {
    invoices: IInvoice[];
    onDetailClick: (items: IInvoice) => void;
}

export const InvoiceListTemplate = (props: IInvoiceListTemplateProps) => {

    const [invoices, setInvoices] = useState<IInvoice[]>([]);

    useEffect(() => {
        setInvoices(props.invoices.slice().sort((a, b) => {
            return (a.invoiceName).localeCompare(b.invoiceName) === 1 ? 1 : -1;
        }))
    }, [props.invoices])

    return (
        <div className="w-100 ">
            <h4>Seznam faktur</h4>
            <div className="mt-2">
                <table>
                    <thead>
                    <tr>
                        <th>Název</th>
                        <th>Stav</th>
                        <th/>
                    </tr>
                    </thead>
                    <tbody>
                    {invoices.map((item) => (
                        <tr>
                            <td>{item.invoiceName}</td>
                            <td>{InvoiceState[item.state]}</td>
                            <td>
                                <button type="button" onClick={() => props.onDetailClick(item)}>
                                    Zobrazit detail
                                </button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </div>
    )
}