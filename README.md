# payme


example query

{
    "start": 0,
    "row": 10,
    "query": [
        "*:*"
    ],
    "filterQuery": [
        "paymentcurrency:EUR"
    ],
    "facets": {
        "limit": 0,
        "pivotMinCount": 0,
        "pivots": [
            {
                "fields": [
                    "nettingkey"
                ],
                "localParameters": [
                    {
                        "localParamKey": "!stats",
                        "localParamValue": "piv1"
                    }
                ]
            }
        ]
    },
    "stats": [
        {
            "field": "paymentamount",
            "localParameters": [
                {
                    "localParamKey": "!tag",
                    "localParamValue": "piv1"
                },
                {
                    "localParamKey": "sum",
                    "localParamValue": "true"
                }
            ]
        },
        {
            "field": "comment",
            "localParameters": [
                {
                    "localParamKey": "!tag",
                    "localParamValue": "piv1"
                },
                {
                    "localParamKey": "distinctValues",
                    "localParamValue": "true"
                }
            ]
        },
        {
            "field": "paymenttype",
            "localParameters": [
                {
                    "localParamKey": "!tag",
                    "localParamValue": "piv1"
                },
                {
                    "localParamKey": "distinctValues",
                    "localParamValue": "true"
                }
            ]
        },
        {
            "field": "id",
            "localParameters": [
                {
                    "localParamKey": "!tag",
                    "localParamValue": "piv1"
                },
                {
                    "localParamKey": "distinctValues",
                    "localParamValue": "true"
                }
            ]
        }
    ],
    "fields": null
}
