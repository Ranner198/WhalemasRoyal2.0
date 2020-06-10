using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class NetworkManagerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 5;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private float _timer;
		public event FieldEvent<float> timerChanged;
		public InterpolateFloat timerInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float timer
		{
			get { return _timer; }
			set
			{
				// Don't do anything if the value is the same
				if (_timer == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_timer = value;
				hasDirtyFields = true;
			}
		}

		public void SettimerDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_timer(ulong timestep)
		{
			if (timerChanged != null) timerChanged(_timer, timestep);
			if (fieldAltered != null) fieldAltered("timer", _timer, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			timerInterpolation.current = timerInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _timer);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_timer = UnityObjectMapper.Instance.Map<float>(payload);
			timerInterpolation.current = _timer;
			timerInterpolation.target = _timer;
			RunChange_timer(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _timer);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (timerInterpolation.Enabled)
				{
					timerInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					timerInterpolation.Timestep = timestep;
				}
				else
				{
					_timer = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_timer(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (timerInterpolation.Enabled && !timerInterpolation.current.UnityNear(timerInterpolation.target, 0.0015f))
			{
				_timer = (float)timerInterpolation.Interpolate();
				//RunChange_timer(timerInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public NetworkManagerNetworkObject() : base() { Initialize(); }
		public NetworkManagerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public NetworkManagerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
