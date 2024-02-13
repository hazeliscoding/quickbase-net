using System;
using System.Collections.Generic;
using QuickbaseNet.Models;
using QuickbaseNet.Requests;

namespace QuickbaseNet.Helpers
{
    public class QuickbaseCommandBuilder
    {
        private string _tableId;
        private string _whereClauseForDeletion;
        private readonly List<Dictionary<string, FieldValue>> _records = new List<Dictionary<string, FieldValue>>();
        private int[] _fieldsToReturn;

        public QuickbaseCommandBuilder ForTable(string tableId)
        {
            _tableId = tableId;
            return this;
        }

        public QuickbaseCommandBuilder ReturnFields(params int[] fieldIds)
        {
            _fieldsToReturn = fieldIds;
            return this;
        }

        public QuickbaseCommandBuilder AddNewRecord(Action<RecordBuilder> config)
        {
            var recordBuilder = new RecordBuilder();
            config(recordBuilder);
            _records.Add(recordBuilder.Build());
            return this;
        }

        public QuickbaseCommandBuilder UpdateRecord(int recordId, Action<RecordBuilder> config)
        {
            var recordBuilder = new RecordBuilder();
            config(recordBuilder);

            // Assuming '3' is the default key field ID for record ID
            recordBuilder.AddField("3", recordId.ToString());
            _records.Add(recordBuilder.Build());
            return this;
        }

        public QuickbaseCommandBuilder WithDeletionCriteria(string whereClause)
        {
            _whereClauseForDeletion = whereClause;
            return this;
        }

        public InsertOrUpdateRecordRequest BuildInsertUpdateCommand()
        {
            return new InsertOrUpdateRecordRequest
            {
                To = _tableId,
                Data = _records,
                FieldsToReturn = _fieldsToReturn
            };
        }

        public DeleteRecordRequest BuildDeleteCommand()
        {
            return new DeleteRecordRequest
            {
                From = _tableId,
                Where = _whereClauseForDeletion
            };
        }

        public class RecordBuilder
        {
            private readonly Dictionary<string, FieldValue> _fields = new Dictionary<string, FieldValue>();

            public RecordBuilder AddField<T>(int fieldId, T value)
            {
                _fields[fieldId.ToString()] = new FieldValue { Value = value }; 
                return this;
            }

            public Dictionary<string, FieldValue> Build()
            {
                return _fields;
            }
        }
    }
}
